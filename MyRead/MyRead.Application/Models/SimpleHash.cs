namespace MyRead.Application.Models
{
    public struct SimpleHash
    {
        public ushort Code { get; }

        public SimpleHash(string value)
        {
            ushort hash = 0;
            if (!string.IsNullOrWhiteSpace(value))
            {
                checked
                {
                    for (int i = 0; i < value.Length; i++)
                        hash += (ushort)(value[i] * i);

                    Code = hash;
                }
            }

            Code = hash;
        }
    }
}