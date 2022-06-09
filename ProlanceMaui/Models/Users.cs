using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlanceMaui.Models
{
    [FirestoreData]
    public class Users
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty("FirstName")]
        public string FirstName { get; set; }
        [FirestoreProperty("LastName")]
        public string LastName { get; set; }
        [FirestoreProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [FirestoreProperty("Email")]
        public string Email { get; set; }
        [FirestoreProperty("Address")]
        public string Address { get; set; }
        [FirestoreProperty("ImageUrl")]
        public string ImageUrl { get; set; }
        [FirestoreProperty("Role")]
        public string Role { get; set; }
    }
}
