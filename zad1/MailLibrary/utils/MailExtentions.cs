namespace MailLibrary.Utils;
internal static class MailExtentions
{
    public static String GetNameFromEmail(this string email)
    {
        var index = email.IndexOf('@');
        if (index == -1)
        {
            return email;
        }
        return email.Substring(0, index);
    }
}