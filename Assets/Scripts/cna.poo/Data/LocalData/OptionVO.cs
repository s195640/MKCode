namespace cna.poo {
    public class OptionVO {
        private string text;
        private Image_Enum image;

        public OptionVO(string text, Image_Enum image) {
            this.text = text;
            this.image = image;
        }

        public string Text { get => text; set => text = value; }
        public Image_Enum Image { get => image; set => image = value; }
    }
}
