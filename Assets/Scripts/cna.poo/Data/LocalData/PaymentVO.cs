namespace cna.poo {
    public class PaymentVO {
        public enum PaymentType_Enum {
            NA,
            Crystal,
            Mana,
            ManaPool,

        }

        private Crystal_Enum mana;
        private PaymentType_Enum paymentType;
        private Crystal_Enum manaUsedAs;

        public PaymentVO(Crystal_Enum mana, PaymentType_Enum paymentType) {
            this.mana = mana;
            this.paymentType = paymentType;
        }

        public Crystal_Enum Mana { get => mana; set => mana = value; }
        public PaymentType_Enum PaymentType { get => paymentType; set => paymentType = value; }
        public Crystal_Enum ManaUsedAs { get { return manaUsedAs == Crystal_Enum.NA ? mana : manaUsedAs; } set => manaUsedAs = value; }
    }
}
