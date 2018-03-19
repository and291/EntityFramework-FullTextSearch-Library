namespace fts_lib.Model
{
    public abstract class FtsParameter : Parent
    {
        public string Search { get; }

        protected FtsParameter(string search)
        {
            Search = search;
        }

        public override string ToString()
        {
            return ValueWrapper.Wrap(Prefix, Search);
        }
    }
}