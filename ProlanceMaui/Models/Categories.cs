using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlanceMaui.Models
{
    [FirestoreData]
    public class Categories
    {
        [FirestoreDocumentId]
        public string Id { get; set; }
        [FirestoreProperty("name")]
        public string Name { get; set; }
        [FirestoreProperty("description")]
        public string Description { get; set; }
        [FirestoreProperty("download_url")]
        public string ImgUrl { get; set; }
    }
}
