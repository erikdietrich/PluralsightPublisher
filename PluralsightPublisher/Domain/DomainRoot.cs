using System;
using System.Collections.Generic;
using System.Linq;

namespace PluralsightPublisher.Domain
{
    public class DomainRoot
    {
        private Project _root;

        public virtual void SetRoot(Project project)
        {
            _root = project;
        }

        public virtual Project GetRoot()
        {
            return _root;
        }
    }
}
