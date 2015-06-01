using System;

namespace NStructurizr.Core.Model
{
    public interface IdGenerator
    {

        String generateId(Element element);

        String generateId(Relationship relationship);

        void found(String id);

    }
}