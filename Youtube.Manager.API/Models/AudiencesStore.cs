using System;
using System.Security.Cryptography;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Youtube.Manager.Core;
using Youtube.Manager.Models.Container;

namespace Youtube.Manager.API.Models
{
    public static class AudiencesStore
    {
        public static Audience AddAudience(string name)
        {
            using (var db = new DbRepository())
            {
                var au = FindAudienceByName(name);
                if (au != null)
                    return au;

                var clientId = Guid.NewGuid().ToString("N");
                var key = new byte[32];
                RandomNumberGenerator.Create().GetBytes(key);
                var base64Secret = TextEncodings.Base64Url.Encode(key);

                var newAudience = new Audience { ClientId = clientId, Base64Secret = base64Secret, Name = name };
                db.Save(newAudience).SaveChanges();
                return newAudience;
            }
        }

        public static Audience FindAudience(string clientId)
        {
            using (var db = new DbRepository())
                return db.Get<Audience>().Where(x => x.ClientId == clientId).ExecuteFirstOrDefault();
        }

        public static Audience FindAudienceByName(string name)
        {
            using (var db = new DbRepository())
                return db.Get<Audience>().Where(x => x.Name == name).ExecuteFirstOrDefault();
        }
    }
}
