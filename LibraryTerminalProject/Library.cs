using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LibraryTerminalProject
{
     class Library
    {
        public string Title { get; set; }
        public string Status { get; set; }

        public virtual void CheckOutItem()
        {
            
        }

        public virtual string ReturnItem()
        {
            string line = "Sup";
            return line;
        }

        public virtual string SearchFor()
        {
            string line = "Hi";
            return line;
        }

    } 
}



