﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Network.Models;
using MNM = Microsoft.Azure.Management.Network.Models;

namespace Microsoft.Azure.Commands.Network
{
    [Cmdlet(VerbsCommon.Add, "AzureRmApplicationGatewayProbeConfig"), OutputType(typeof(PSApplicationGateway))]
    [CliCommandAlias("applicationgateway;probe;config;add")]
    public class AddAzureApplicationGatewayProbeConfigCommand : AzureApplicationGatewayProbeConfigBase
    {
        [Parameter(
             Mandatory = true,
             ValueFromPipeline = true,
             HelpMessage = "The applicationGateway")]
        public PSApplicationGateway ApplicationGateway { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            var probe = this.ApplicationGateway.Probes.SingleOrDefault(resource => string.Equals(resource.Name, this.Name, System.StringComparison.CurrentCultureIgnoreCase));

            if (probe != null)
            {
                throw new ArgumentException("Probe with the specified name already exists");
            }

            probe = base.NewObject();
            this.ApplicationGateway.Probes.Add(probe);

            WriteObject(this.ApplicationGateway);
        }
    }
}