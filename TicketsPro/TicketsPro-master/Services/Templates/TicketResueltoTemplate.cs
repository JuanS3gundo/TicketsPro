using System;
namespace Services.Templates
{
    public static class TicketResueltoTemplate
    {
        public static string Generar(
            string nombreUsuario,
            string nombreTecnico,
            string titulo,
            string descripcionOriginal,
            string solucion,
            DateTime fechaCierre,
            Guid idTicket)
        {
            return $@"
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            color: #333333;
            margin: 0;
            padding: 0;
        }}
        .container {{
            background-color: #ffffff;
            max-width: 600px;
            margin: 30px auto;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 6px rgba(0,0,0,0.1);
        }}
        h2 {{
            color: #004aad;
            margin-bottom: 10px;
        }}
        p {{
            line-height: 1.6;
        }}
        ul {{
            list-style: none;
            padding: 0;
        }}
        li {{
            margin-bottom: 8px;
        }}
        .footer {{
            margin-top: 25px;
            font-size: 0.9em;
            color: #777777;
            border-top: 1px solid #e0e0e0;
            padding-top: 10px;
        }}
        .highlight {{
            background-color: #e8f0fe;
            padding: 10px;
            border-radius: 6px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h2>Tu ticket #{idTicket} ha sido resuelto</h2>
        <p>Hola <strong>{nombreUsuario}</strong>,</p>
        <p>El tecnico <strong>{nombreTecnico}</strong> ha resuelto tu ticket:</p>
        <div class='highlight'>
            <ul>
                <li><strong>Titulo:</strong> {titulo}</li>
                <li><strong>Descripcion original:</strong> {descripcionOriginal}</li>
                <li><strong>Solucion:</strong> {solucion}</li>
                <li><strong>Fecha de resolucion:</strong> {fechaCierre:dd/MM/yyyy HH:mm}</li>
            </ul>
        </div>
        <p>Si necesitas hacer alguna consulta adicional, responde a este correo o abri un nuevo ticket desde el sistema.</p>
        <div class='footer'>
            <p>Equipo de Soporte Tecnico<br/>
            TicketPro © {DateTime.Now.Year}</p>
        </div>
    </div>
</body>
</html>";
        }
    }
}
