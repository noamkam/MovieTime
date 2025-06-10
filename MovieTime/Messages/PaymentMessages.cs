namespace MovieTime.Messages
{
    public class PaymentMessages
    {
        public const string CodeInvalid = "קוד הקופון אינו תקין";
        public const string DetailsInvalid = "אחד או יותר מהפרטים שהזנת לא נכונים";
        public const string NotEnoughBalance = "אין מספיק כסף בחשבון כדי לבצע את הרכישה";

        public const string AccountNumberNull = "הכנס מספר חשבון";
        public const string CreditCardNumberNull = "הכנס מספר כרטיס אשראי";
        public const string OwnerIdNull = "הכנס מספר תעודת זהות של בעל החשבון";
        public const string CVVNull = " הכנס שלוש ספרות בגב הכרטיס ";
        public const string ValidityMonthNull = "הכנס חודש תוקף כרטיס אשראי";
        public const string ValidityYearNull = "הכנס שנת תוקף כרטיס אשראי";
    }

}
