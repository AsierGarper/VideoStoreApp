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

        public Film (int fiml_id, string title, string synopsis, int recommendedAge, bool avaliable)
        {
            Film_id = fiml_id;
            Title = title;
            Synopsis = synopsis;
            RecommendedAge = recommendedAge;
            Available = avaliable;
        }
    }
}
