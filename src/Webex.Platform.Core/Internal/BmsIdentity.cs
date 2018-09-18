using System;
using System.Collections.Generic;
using System.Text;

namespace Webex.Platform.Internal
{
    public class BmsIdentity : IAppIdentity
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Nickname { get; set; }

        public int IdentityType { get; set; }
    }
}
