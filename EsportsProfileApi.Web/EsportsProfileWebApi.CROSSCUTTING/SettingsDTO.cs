﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsportsProfileWebApi.CROSSCUTTING
{
    public class SettingsDTO
    {
        public decimal Sensitivity { get; set; }
        public int Dpi { get; set; }

        public string Mouse { get; set; } = string.Empty;

    }
}
