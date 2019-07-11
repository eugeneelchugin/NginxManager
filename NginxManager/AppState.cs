using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace NginxManager
{
    public class AppState
    {
        public AppState(bool nginxIsStarted)
        {
            NginxIsStarted = new BehaviorSubject<bool>(nginxIsStarted);
        }

        public BehaviorSubject<bool> NginxIsStarted { get; }
    }
}