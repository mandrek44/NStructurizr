using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NStructurizr.Core.Model
{
    /**
 * Sorry about the name, send a pull request if you think of something better! :-)
 */

    public abstract class TaggableThing
    {
        private ISet<String> _tags = new HashSet<string>();

        public String tags
        {
            get
            {
                if (!this._tags.Any())
                {
                    return "";
                }

                StringBuilder buf = new StringBuilder();
                foreach (String tag in _tags)
                {
                    buf.Append(tag);
                    buf.Append(",");
                }

                String tagsAsString = buf.ToString();
                return tagsAsString.Substring(0, tagsAsString.Length - 1);
            }
        }

        void setTags(String tags)
        {
            if (tags == null)
            {
                return;
            }

            this._tags.Clear();
            foreach (var tag in tags.Split(','))
            {
                this._tags.Add(tag);
            }
        }

        public void addTags(params string[] tags)
        {
            if (tags == null)
            {
                return;
            }

            foreach (String tag in tags)
            {
                if (tag != null)
                {
                    this._tags.Add(tag);
                }
            }
        }

    }

    /**
 * These are the types of elements that are permitted in a model.
 */

    /**
 * This represents a software system, which itself can be made up of
 * a number of containers.
 */


    /**
 * This is the superclass for all model elements.
 */


    // TODO: Must be synchronized

    /**
 * This is the starting point for creating a software architecture
 * model - everything is attached to an instance of this.
 */
}
