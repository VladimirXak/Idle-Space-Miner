namespace Game.Data
{
    [System.Serializable]
    public struct ScientificNotationData
    {
        public double Mantissa;
        public int Order;

        public ScientificNotationData(ScientificNotation value)
        {
            Mantissa = value.Mantissa;
            Order = value.Order;
        }

        public ScientificNotation ToScn()
        {
            return new ScientificNotation(Mantissa, Order);
        }
    }
}
