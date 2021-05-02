namespace BlaBlaPrincess.SecretsExplorer.Data
{
    public interface ISecret
    {
        public string Name { get; set; }
        public int Weight { get; }
    }
}