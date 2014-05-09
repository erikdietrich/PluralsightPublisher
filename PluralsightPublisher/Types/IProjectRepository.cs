﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluralsightPublisher.DataTransfer;

namespace PluralsightPublisher.Types
{
    public interface IProjectRepository
    {
        Project GetById(string id);

        void Save(Project itemToCreate);
    }
}