using DAL.Models;
using System;

namespace hoanglee.Models
{
    public class CurrentUserStore
    {
        private static CurrentUserStore _instance;
        private static readonly object _lock = new object();

        public Customer CurrentUser { get; private set; }
        public bool IsAdmin { get; private set; }
        public bool IsLoggedIn => CurrentUser != null;

        private CurrentUserStore() { }

        public static CurrentUserStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CurrentUserStore();
                        }
                    }
                }
                return _instance;
            }
        }

        public void SetCurrentUser(Customer user, bool isAdmin)
        {
            CurrentUser = user;
            IsAdmin = isAdmin;
        }

        public void Logout()
        {
            CurrentUser = null;
            IsAdmin = false;
        }
    }
}