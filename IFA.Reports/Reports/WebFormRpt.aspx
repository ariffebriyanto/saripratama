﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WebFormRpt.aspx.vb" Inherits="IFA.Reports.WebFormRpt" %>
<%@ Register assembly="Stimulsoft.Report.Web, Version=2014.1.1900.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a" namespace="Stimulsoft.Report.Web" tagprefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
    <div>
    
    </div>
        <cc1:StiWebViewer ID="StiWebViewer1" runat="server"  
            OnInteraction="StiWebViewer1_Interaction">
        </cc1:StiWebViewer>
    </form>
</body>
</html>
