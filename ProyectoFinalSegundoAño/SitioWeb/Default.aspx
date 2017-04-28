<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageGeneral.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content" ContentPlaceHolderID="cphContenido" Runat="Server">
    <div id="contenido" align="center">
        <br />
        <span class="style1" style="font-size: x-large"><strong>¡Bienvenid@ al 10 de 
        platino!</strong></span><br />
        Inicia sesión para realizar jugadas.<br />
        <br />
            <asp:Login ID="LoginJugador" runat="server" BackColor="#F7F6F3" 
            BorderColor="#E6E2D8" BorderStyle="Solid" BorderWidth="1px" 
            DisplayRememberMe="False" ForeColor="#333333" TitleText="" 
            onauthenticate="LoginJugador_Authenticate" BorderPadding="4" 
            Font-Names="Verdana" Font-Size="0.9em" Height="81px" Width="320px">
                <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
                    BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
                <TextBoxStyle Font-Size="0.8em" />
                <TitleTextStyle Font-Bold="True" />
            </asp:Login>
         <asp:Label ID="lblMensaje" runat="server"></asp:Label>
         <br />
        <span style="font-size: small">Si no eres un jugador registrado puedes hacerlo&nbsp; 
        </span>
        <asp:LinkButton ID="LinkButton1" runat="server" 
            PostBackUrl="~/RegistroJugador.aspx" style="font-size: small">aquí</asp:LinkButton>
&nbsp;<span style="font-size: small">o en la pestaña Registrarse.</span><br />
         <br />
    </div>
</asp:Content>

