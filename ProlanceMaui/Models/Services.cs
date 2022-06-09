using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlanceMaui.Models
{
    [FirestoreData]
    public class Services
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty("Name")]
        public string Name { get; set; }
        [FirestoreProperty("Category")]
        public string CategoryId { get; set; }
    }
}
