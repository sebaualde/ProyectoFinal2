<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageGeneral.master" AutoEventWireup="true" CodeFile="DatosJugador.aspx.cs" Inherits="DatosJugador" %>

<asp:Content ID="Content" ContentPlaceHolderID="cphContenido" Runat="Server">
    <p>
        <br />
        <table align="center" style="width: 461px;">
            <tr>
                <td style="width: 203px; text-align: center;">
                    CI:</td>
                <td style="width: 252px; text-align: center;">
                    <asp:TextBox ID="txtCI" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center; height: 26px;">
                    Nombre Completo:</td>
                <td style="width: 252px; text-align: center; height: 26px;">
                    <asp:TextBox ID="txtNomCompleto" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    Nombre Logueo:</td>
                <td style="width: 252px; text-align: center;">
                    <asp:TextBox ID="txtNomLogueo" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    Contraseña:</td>
                <td style="width: 252px; text-align: center;">
                    <asp:TextBox ID="txtContrasenia" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    Numero de cuenta bancaria:</td>
                <td style="width: 252px; text-align: center;">
                    <asp:TextBox ID="txtCuentaBancaria" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    &nbsp;Banco:</td>
                <td style="width: 252px; text-align: center;">
                    <asp:DropDownList ID="ddlBanco" runat="server" Height="23px" Width="259px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    &nbsp;</td>
                <td style="width: 252px; text-align: center;">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 203px; text-align: center;">
                    &nbsp;</td>
                <td style="width: 252px; text-align: center;">
                    <asp:Button ID="btnModificar" runat="server" onclick="btnModificar_Click" 
                        Text="Modificar" />
&nbsp;&nbsp;
        <asp:Button ID="btnEliminar" runat="server" onclick="btnEliminar_Click" 
                        Text="Eliminar" />
&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" onclick="btnCancelar_Click" 
                        Text="Cancelar" />
                </td>
            </tr>
        </table>
        <br />
</p>
</asp:Content>

