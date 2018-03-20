namespace fts_lib.Model
{
    public abstract class FtsParameter
    {
        public string Prefix { get; }
        public string Search { get; }

        protected FtsParameter(string search)
        {
            Prefix = Storage.Instance
                .GetPrefixForType(GetType());
            Search = search;
        }

        public override string ToString()
        {
            return ValueWrapper.Wrap(Prefix, Search);
        }
    }
}