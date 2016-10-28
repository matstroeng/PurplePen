﻿/* Copyright (c) 2006-2008, Peter Golde
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without 
 * modification, are permitted provided that the following conditions are 
 * met:
 * 
 * 1. Redistributions of source code must retain the above copyright
 * notice, this list of conditions and the following disclaimer.
 * 
 * 2. Redistributions in binary form must reproduce the above copyright
 * notice, this list of conditions and the following disclaimer in the
 * documentation and/or other materials provided with the distribution.
 * 
 * 3. Neither the name of Peter Golde, nor "Purple Pen", nor the names
 * of its contributors may be used to endorse or promote products
 * derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
 * USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY
 * OF SUCH DAMAGE.
 */
 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PurplePen
{
    public partial class TeamVariationsForm: PurplePen.BaseDialog
    {
        public EventHandler<GetReportBodyEventArgs> GetReportBody;

        public TeamVariationsForm()
        {
            InitializeComponent();
        }

        public int NumberOfTeams { 
            get
            {
                return (int) upDownNumberOfTeams.Value;
            }
            set
            {
                upDownNumberOfTeams.Value = value;
            }
        }

        public int NumberOfLegs
        {
            get
            {
                return (int)upDownNumberOfLegs.Value;
            }
            set
            {
                upDownNumberOfLegs.Value = value;
            }
        }

        void SetReportBody(string title, string body)
        {
            string styles = "";
            string htmlText = ReportForm.htmlTemplate.Replace("<!--@@TITLE@@-->", title).Replace("<!--@@STYLES@@-->", styles).Replace("<!--@@BODY@@-->", body);
            webBrowser.DocumentText = htmlText;
        }

        public class GetReportBodyEventArgs: EventArgs
        {
            public int NumberOfTeams;
            public int NumberOfLegs;
            public string ReportBody;

            public GetReportBodyEventArgs(int numberOfTeams, int numberOfLegs)
            {
                NumberOfTeams = numberOfTeams;
                NumberOfLegs = numberOfLegs;
                ReportBody = "";
            }
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            GetReportBodyEventArgs eventArgs = new GetReportBodyEventArgs(NumberOfTeams, NumberOfLegs);
            GetReportBody?.Invoke(this, eventArgs);

            SetReportBody(this.Text, eventArgs.ReportBody);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            webBrowser.ShowPrintDialog();
        }

        private void buttonPrintPreview_Click(object sender, EventArgs e)
        {
            webBrowser.ShowPrintPreviewDialog();
        }
    }
}
