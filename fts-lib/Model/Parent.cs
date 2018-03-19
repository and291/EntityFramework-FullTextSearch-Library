namespace fts_lib.Model
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