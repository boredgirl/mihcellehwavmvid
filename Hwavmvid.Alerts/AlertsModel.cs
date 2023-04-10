﻿using System;

namespace Mihcelle.Hwavmvid.Alerts
{
    public class AlertsModel
    {

        public string Id { get; set; }

        public string Message { get; set; }

        public string Headline { get; set; }

        public PositionType Position { get; set; }

        public bool ConfirmDialog { get; set; }

        public DateTime CreatedOn { get; set; }

    }
}
