using ConsoleApp1;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, false)
    .Build();

//var DockerHostMachineIpAddress = Dns.GetHostAddresses(new Uri("http://docker.for.win.localhost").Host)[0].ToString();
var cadena = configuration.GetConnectionString("cn");

var USER_SERVER = configuration.GetSection("USER_SERVER").Value;
var PASSWORD_SERVER = configuration.GetSection("PWD_SERVER").Value;

var FECHA_INICIO = configuration.GetSection("FECHA_INICIO").Value;
var FECHA_FIN = configuration.GetSection("FECHA_FIN").Value;


List<Employee> lista = new List<Employee>();
using (var conexion = new SqlConnection(cadena))
{
    conexion.Open();
    SqlCommand cmd = new SqlCommand("SP_EMPLOYEE", conexion);
    cmd.CommandType = System.Data.CommandType.StoredProcedure;
    cmd.Parameters.Add("@ACCION", System.Data.SqlDbType.VarChar, 30).Value = "REPORTE";
    cmd.Parameters.Add("@DATE_START", System.Data.SqlDbType.Date).Value = FECHA_INICIO;
    cmd.Parameters.Add("@DATE_END", System.Data.SqlDbType.Date).Value = FECHA_FIN;

    using (var dr = await cmd.ExecuteReaderAsync())
    {
        while (await dr.ReadAsync())
        {
            lista.Add(new Employee
            {
                id = dr["id"].ToString(),
                name = dr["name"].ToString(),
                document_number = dr["document_number"].ToString(),
                salary = dr["salary"].ToString(),
                age = dr["age"].ToString(),
                profile = dr["profile"].ToString(),
                admission_date = Convert.ToDateTime(dr["admission_date"]).ToString("dd/MM/yyyy"),
            }); ;
        }
    }

}

MemoryStream outputStream = new MemoryStream();
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
using (ExcelPackage package = new ExcelPackage(outputStream))
{
    
    ExcelWorksheet facilityWorksheet = package.Workbook.Worksheets.Add("sheetName");
    facilityWorksheet.Cells.LoadFromCollection(lista, true);

    package.Save();
}
outputStream.Position = 0;
Attachment attachment = new Attachment(outputStream, "reporte.xlsx", "application/vnd.ms-excel");

MailMessage mm = new MailMessage();
mm.From = new MailAddress("nicolashidalgocorrea@hotmail.com");
mm.To.Add("franco.paredes@oechsle.pe");
mm.To.Add("nicolashidalgocorrea@gmail.com");
mm.Subject = "Reporte Empleados - Examen Técnico Oechsle";
mm.Body = "Adjunto el reporte";
mm.Attachments.Add(attachment);

//System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
SmtpClient smtp = new SmtpClient("smtp-legacy.office365.com");
//SmtpClient smtp = new SmtpClient("smtp.office365.com");
smtp.Port = 587;
smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
smtp.EnableSsl = true;
smtp.UseDefaultCredentials = false;

if (USER_SERVER == string.Empty || PASSWORD_SERVER == string.Empty)
{
    return;
}
smtp.Credentials = new System.Net.NetworkCredential(USER_SERVER, PASSWORD_SERVER);

await smtp.SendMailAsync(mm);


