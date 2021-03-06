﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageGeneral.master" AutoEventWireup="true" CodeFile="HistoricoJugadasPremiadas.aspx.cs" Inherits="HistoricoJugadasPremiadas" %>



<asp:Content ID="Content" ContentPlaceHolderID="cphContenido" Runat="Server">
    <center><h2>
        <br />
        Historial de jugadas premiadas</h2>
        <p>
        &nbsp;Fecha de sorteo (dd/mm/aaaa):
            <asp:TextBox ID="txtDia" runat="server"></asp:TextBox>
            &nbsp;<asp:Button ID="btnFiltrarPorFecha" runat="server" Text="Filtrar" 
                onclick="btnFiltrarPorFecha_Click" />
            &nbsp;<asp:Button ID="btnQuitarFiltro" runat="server" 
                onclick="btnQuitarFiltro_Click" Text="Quitar Filtro" />
            <br />
    </p>
    <asp:Xml ID="xmlPremiadas" runat="server" 
            TransformSource="~/App_Data/Premiadas.xslt"></asp:Xml>
    <asp:Label ID="lblMensaje" runat="server" EnableViewState="False"></asp:Label>
    </center>
</asp:Content>
