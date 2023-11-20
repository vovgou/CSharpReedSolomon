namespace Backblaze.Erasure
{
    public class OutputByteInputTableCodingLoop : CodingLoopBase
    {
        public override void CodeSomeShards(
            byte[][] matrixRows,
            byte[][] inputs, int inputCount,
            byte[][] outputs, int outputCount,
            int offset, int byteCount)
        {
            byte[][] table = Galois.MULTIPLICATION_TABLE;
            for (int iOutput = 0; iOutput < outputCount; iOutput++)
            {
                byte[] outputShard = outputs[iOutput];
                byte[] matrixRow = matrixRows[iOutput];
                for (int iByte = offset; iByte < offset + byteCount; iByte++)
                {
                    int value = 0;
                    for (int iInput = 0; iInput < inputCount; iInput++)
                    {
                        byte[] inputShard = inputs[iInput];
                        byte[] multTableRow = table[matrixRow[iInput]];
                        value ^= multTableRow[inputShard[iByte]];
                    }
                    outputShard[iByte] = (byte)value;
                }
            }
        }
    }
}
