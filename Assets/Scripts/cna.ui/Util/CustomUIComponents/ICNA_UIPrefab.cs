
namespace cna.ui {
    public interface ICNA_UIPrefab<I> {
        public I Data { get; }
        public void SetupUI(I data);
        public void UpdateUI();
        public void Destroy();
    }
}
