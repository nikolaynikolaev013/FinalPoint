namespace FinalPoint.Common
{
    public class MailConstants
    {
        #region New Parcel

        public const string NewParcelSenderSubject = "Вашата пратка беше приета в офис {0}";

        public const string NewParcelSenderMainText = "Вашата пратка беше приета в офис {0}. </br></br>  Офис за предаване: {1} ";

        public const string NewParcelRecipentSubject = "Пратка за Вас беше приета в офис {0}";

        public const string NewParcelRecipentMainText = "Пратка за Вас беше приета в офис {0}. </br></br> Офис за предаване: {1} ";

        #endregion

        #region Update Parcel

        public const string UpdateParcelSenderSubject = "Пратката Ви е приета в импортният офис.";

        public const string UpdateParcelSenderMainText = "Пратката Ви е приета в импортния офис ({0}).";

        public const string UpdateParcelRecipentSubject = "Пратката Ви е приета в офис {0}";

        public const string UpdateParcelRecipentMainText = "Пратката Ви е приета в офис {0}. Можете да заповядате за да я получите.";

        #endregion

        #region Dispose Parcel

        public const string DisposeParcelSenderMainText = "Пратката Ви беше предадена успешно.";

        public const string DisposeParcelRecipentMainText = "Пратката Ви беше приета.";

        #endregion

        #region Misc

        public const string PriceMainText = "Цена за доставка {0:F2} </br><b> Обща цена: {1:F2}<b>";

        #endregion
    }
}
