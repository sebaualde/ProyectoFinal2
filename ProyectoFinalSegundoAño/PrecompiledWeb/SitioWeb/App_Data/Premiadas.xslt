<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:template match="/">
    <div class="tablaListados">
    <table >
      <!--Creo la fila con los títulos de cada columna-->
      <thead><tr>
        <th>ID</th>
        <th>Fecha y Hora Jugada</th>
        <th>Fecha y Hora Sorteo</th>
        <!--<th>Jugador</th>-->
      </tr>
      </thead>
      <tbody>
      <xsl:for-each select="JugadasPremiadas/Jugada">
        <!--Para cada ingreso el ID, Fecha y Hora, Fecha de sorteo y jugador en la columna correspondiente -->
        <tr>
          
          <td rowspan="2" id="id">
              <xsl:value-of select="Id" />
          </td>
          <td>
            <xsl:value-of select="FechaYHora" />
          </td>
          <td>
            <xsl:value-of select="Sorteo" />
          </td>

          <!--<td>
            <xsl:value-of select="Jugador" />
          </td>-->
        </tr>
        <tr class="alt">
          <td colspan="4"><center>
          Números: | <xsl:for-each select="Numeros/Numero">
              
                 <xsl:value-of select="."/>
               |
              
          </xsl:for-each></center>
          </td>
        </tr>
        
      </xsl:for-each>
        </tbody>
    </table>
    </div>
  </xsl:template>
</xsl:stylesheet>
