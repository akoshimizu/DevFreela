﻿using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated
{
    public class FreelancerNotificationHandler : INotificationHandler<ProjectCreatedNotification>
    {
        public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
        {
            //realiza a implementação desejada...  ex: email
            Console.WriteLine($"Enviando notificação aos freelancers sobre o projeto {notification.Title}.");

            return Task.CompletedTask;
        }
    }
}
