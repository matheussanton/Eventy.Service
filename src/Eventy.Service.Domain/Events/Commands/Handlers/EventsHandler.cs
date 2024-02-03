using System.Net;
using Eventy.Service.Domain.Emailing.Interfaces;
using Eventy.Service.Domain.Entities;
using Eventy.Service.Domain.Events.Interfaces;
using Eventy.Service.Domain.Responses;
using Eventy.Service.Domain.Responses.Enums;
using Eventy.Service.Domain.User.Interfaces;
using Eventy.Service.Domain.User.Models;
using MediatR;

namespace Eventy.Service.Domain.Events.Commands.Handlers
{
    public class EventsHandler : IRequestHandler<CreateEventCommand>,
                                    IRequestHandler<UpdateEventCommand>,
                                    IRequestHandler<DeleteEventCommand>,
                                    IRequestHandler<UpdateEventStatusCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailingService _emailingService;
         private readonly Response _response;

        public EventsHandler(
            IEventRepository eventRepository,
            IUserRepository userRepository,
            IEmailingService emailingService,
            Response response
        )
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _emailingService = emailingService;
            _response = response;
        }
        
        
        public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = request.Parse();

            await _eventRepository.CreateAsync(eventEntity);

            await SendInvitationEmail(request, eventEntity);

            return;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var record = await _eventRepository.GetByIdAsync(request.Id, request.UserId);

            if (record == null)
            {
                throw new Exception("Event not found");
            }

            var entity = request.Parse(record);

            await _eventRepository.UpdateAsync(entity);

            return;
        }

        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var record = await _eventRepository.GetByIdAsync(request.Id, request.UserId);

            if (record == null)
            {
                request.AddNotification("Id", "Event not found");
                _response.Send(ResponseStatus.Fail, HttpStatusCode.BadRequest, request.Notifications);
            }

            await _eventRepository.DeleteAsync(request.Id, request.UserId);

            _response.Send(ResponseStatus.Success, HttpStatusCode.OK);
        }

        public async Task Handle(UpdateEventStatusCommand request, CancellationToken cancellationToken)
        {
           var record = await _eventRepository.GetByIdAsync(request.EventId, request.UserId);

            if (record == null)
            {
                request.AddNotification("Id", "Event not found");
                _response.Send(ResponseStatus.Fail, HttpStatusCode.BadRequest, request.Notifications);
            }

            await _eventRepository.UpdateStatusAsync(request.EventId, request.UserId, request.Status);

             _response.Send(ResponseStatus.Success, HttpStatusCode.OK);
        }


        private async Task SendInvitationEmail(BaseEventCommand request, EventEntityDomain? eventEntity)
        {
            if(eventEntity == null) return;

            string eventyEmail = "naoresponda.convite@eventy.com";
            string subject = "Convite para evento";

            List<Guid> participantsIds = new(eventEntity.UserEvents.Select(ue => ue.UserId));
            var participants = await _userRepository.GetByIdAsync(participantsIds);

            SelectUser sender = participants?.FirstOrDefault(p => p.Id == request.UserId)!;
            List<SelectUser> recipients = participants?.Where(p => request.Participants.Contains(p.Id))?.ToList()!;


            foreach (var recipient in recipients)
            {
               string body = $@"
                <!DOCTYPE html>
                <html lang='en'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <style>
                        body {{
                            font-family: 'Arial', sans-serif;
                            background-color: #f4f4f4;
                        }}
                        .container {{
                            max-width: 600px;
                            background-color: #fff;
                            border-radius: 10px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        h1 {{
                            color: #333;
                        }}
                        p {{
                            color: #666;
                        }}
                        .button {{
                            display: inline-block;
                            padding: 5px 20px 10px 20px;
                            background-color: #007bff;
                            color: #fff;
                            text-decoration: none;
                            border-radius: 5px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <h1>Olá, {recipient.Name}!</h1>
                        <p>{sender.Name} convidou você para o evento {eventEntity.Name}
                        que começará em {eventEntity.StartDate:dd/MM/yyyy} às {eventEntity.StartDate:HH:mm}
                        no local {eventEntity.Location}.</p>
                        <p>Confirme sua presença clicando no botão abaixo:</p>
                        <a class='button' href='SEU_LINK_DE_CONFIRMACAO'>Confirmar Presença</a>
                    </div>
                </body>
                </html>
            ";


                _emailingService.Send(eventyEmail, recipient.Email, subject, body);
            }

        }

    }
}
