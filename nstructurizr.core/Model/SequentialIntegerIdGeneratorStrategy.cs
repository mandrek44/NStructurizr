using System;

namespace NStructurizr.Core.Model
{
    class SequentialIntegerIdGeneratorStrategy : IdGenerator
    {

        private int ID = 0;

        public void found(string id)
        {
            int idAsInt = int.Parse(id);
            if (idAsInt > ID)
            {
                ID = idAsInt;
            }
        }


        public string generateId(Element element)
        {
            return "" + ++ID;
        }

        public String generateId(Relationship relationship)
        {
            return "" + ++ID;
        }

    }
}