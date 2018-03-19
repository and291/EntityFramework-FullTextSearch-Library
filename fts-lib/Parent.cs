namespace fts_lib
{
    public abstract class Parent
    {
        public string Prefix { get; }

        protected Parent()
        {
            Prefix = Prefixes.Instance.GetPrefixByImplementedInterface(GetType());
        }
    }
}