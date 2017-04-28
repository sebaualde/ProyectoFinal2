<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageGeneral.master" AutoEventWireup="true" CodeFile="ListarSorteosParticipados.aspx.cs" Inherits="ListarSorteosParticipados" %>

<asp:Content ID="Content" ContentPlaceHolderID="cphContenido" Runat="Server">
    <p>
        <br />
        <span style="font-size: x-large"><strong>Estos son los sorteos en los cuales participa.</strong></span></p>
    <p style="margin: 2px">

        <table align="center" style="border-color: #99FF99; border-style: ridge;">
            <tr>
                <td style="border: 0px hidden #99FFCC; background-color: #CCFFFF">
                    Filtrar por:&nbsp;&nbsp;
                </td>
                <td style="border: thin hidden #99FFCC; background-color: #CCFFFF">
                    Fecha Buscada <span style="font-size: xx-small">(Ej: DD/MM/AAAA)</span></td>
                <td style="border: 0px hidden #99FFCC; background-color: #CCFFFF">
                    &nbsp;</td>
                <td style="border: 0px hidden #99FFCC; background-color: #CCFFFF">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 82px">
                    <asp:DropDownList ID="ddlTipoFiltro" runat="server">
                        <asp:ListItem>Ninguno</asp:ListItem>
                        <asp:ListItem Value="MesYAnio">Mes Y Año</asp:ListItem>
                        <asp:ListItem Value="FechaConcreta">Fecha Concreta</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtDia" runat="server" ToolTip="Dia" Width="50px"></asp:TextBox>
                &nbsp;
                    <asp:TextBox ID="txtMes" runat="server" ToolTip="Mes" Width="50px"></asp:TextBox>
&nbsp;
                    <asp:TextBox ID="txtAnio" runat="server" ToolTip="Año" Width="92px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
                </td>
                <td style="width: 64px">
                    <asp:ImageButton ID="ibtnFiltrar" runat="server" 
                        ImageUrl="~/Resources/filter_add.png" onclick="ibtnFiltrar_Click" 
                        ToolTip="Filtrar" />
                </td>
                <td style="width: 64px">
                    <asp:ImageButton ID="ibtnLimpiar" runat="server" Height="32px" 
                        ImageUrl="~/Resources/limpiar.jpg" onclick="ibtnLimpiar_Click" 
                        ToolTip="Limpiar" Width="32px" />
                </td>
            </tr>
        </table>
    </p>
    <p style="margin: 0px; font-size: small">

        Para ver los números jugados en un sorteo haga click en el mismo.</p>
    <p>
        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    </p>
    <p>
        <div class="tablaListados"><asp:GridView ID="gvSorteos" runat="server" align="center" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None" 
            onselectedindexchanged="gvSorteos_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="FechaHoraSorteo" HeaderText="Sorteos" />
                <asp:CommandField AccessibleHeaderText="Números Jugados" 
                    HeaderText="Números Jugados" SelectText="Ver" ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#0066ad" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#B3E4FF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView></div>
    </p>
    <p>
        &nbsp;</p>
</asp:Content>
