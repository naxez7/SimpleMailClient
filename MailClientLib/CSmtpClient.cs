﻿using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClientLib
{
  public class CSmtpClient
  {
    #region / locals /
    private MailAddress objMailTo, objMailFrom;
    private MailMessage objMail;
    private string usr, pass;
    #endregion
    #region / properties /
    public string mailTo { get; set; }
    public string mailFrom { get; set; }
    public string subject { get; set; }
    public string message { get; set; }
    #endregion
    #region / constructor /
    /// <summary>
    /// Constructor for Custom Smtp Client.
    /// </summary>
    /// <param name="usr">Username</param>
    /// <param name="pass">Password</param>
    /// <param name="mailTo">Email address of the recipient</param>
    /// <param name="mailFrom">Who the mail is coming from</param>
    /// <param name="subject">The Subject of the email</param>
    /// <param name="message">The body of the email</param>
    public CSmtpClient(string usr, string pass, string mailTo, string mailFrom, string subject, string message)
    {
      this.usr = usr;
      this.pass = pass;
      this.mailTo = mailTo;
      this.mailFrom = mailFrom;
      this.subject = subject;
      this.message = message;

      objMailTo = new MailAddress(mailTo);
      objMailFrom = new MailAddress(mailFrom);
      objMail = new MailMessage(objMailFrom, objMailTo);
      objMail.Subject = subject;
      objMail.Body = message;
    }
    /// <summary>
    /// Constructor for Custom Smtp Client.
    /// </summary>
    /// <param name="usr">Username</param>
    /// <param name="pass">Password</param>
    /// <param name="mailTo">Email address of the recipient</param>
    /// <param name="mailFrom">Who the mail is coming from</param>
    /// <param name="subject">The Subject of the email</param>
    /// <param name="attachments">Attachemts added to email</param>
    public CSmtpClient(string usr, string pass, string mailTo, string mailFrom, string subject, string message, Attachment[] attachments)
      : this(usr, pass, mailTo, mailFrom, subject, message)
    {
      foreach (Attachment att in attachments)
        objMail.Attachments.Add(att);
      this.usr = usr;
    }
    #endregion
    #region / public /
    /// <summary>
    /// Sends email and disposes of CSmtpClient object.
    /// </summary>
    public void sendMessage()
    {
      SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
      smtp.Credentials = new NetworkCredential(usr, pass);
      smtp.EnableSsl = true;
      smtp.Send(objMail);

      smtp.Dispose();
    }
    #endregion
  }
}
