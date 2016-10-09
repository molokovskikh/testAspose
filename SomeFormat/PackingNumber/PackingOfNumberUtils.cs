
namespace PackingNumber
{
    public static class PackingOfNumberUtils
    {

        /// <summary>
        /// Pack number
        /// </summary>
        /// <param name="number">source number</param>
        /// <param name="lenInBytes">size array of result bytes</param>
        /// <param name="reverse">flag direction passing by cycle</param>
        /// <returns>Arrays of result bytes</returns>
        public static byte[] PackNumber(long number, uint sizeInBytes, bool reverse = false)
        {
            byte[] res = new byte[sizeInBytes];

            for (uint i = 0, j, bPtr; i < sizeInBytes; i++)
            {
                j = reverse ? i : sizeInBytes - 1 - i;
                bPtr = 8 * j;
                res[i] = (byte)(number >> ((int) bPtr & 0xFF));
            }

            return res;
        }



        /// <summary>
        /// Unpack number from bytes
        /// </summary>
        /// <param name="bytes_number">source bytes</param>
        /// <param name="reverse">flag direction passing by cycle</param>
        /// <returns></returns>
        public static long UnpackNumber(byte[] bytes_number, bool reverse = false)
        {
            long res = 0;


            for (uint i = 0, j, bPtr; i < bytes_number.Length; i++)
            {
                j = reverse ? (uint)bytes_number.Length - 1 - i : i;
                bPtr = 8 * (reverse ? i : j);
                res |= ((long)bytes_number[j]) << (int)bPtr;
            }

            return res;
        }
    }
}
