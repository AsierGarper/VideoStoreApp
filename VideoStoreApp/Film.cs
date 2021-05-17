using System;
using System.Collections.Generic;
using System.Text;

namespace VideoStoreApp
{
    class Film
    {
        public int Film_id { get; set; }
        public string Title { get; set; }

        public string Synopsis { get; set; }

        public int RecommendedAge { get; set; }

        public bool Available { get; set; }

        
    }
}
