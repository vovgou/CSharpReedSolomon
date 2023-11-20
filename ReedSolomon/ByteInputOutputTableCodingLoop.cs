namespace Backblaze.Erasure
{
    public class ByteInputOutputTableCodingLoop : CodingLoopBase
    {
        public override void CodeSomeShards(
                byte[][] matrixRows,
                byte[][] inputs, int inputCount,
                byte[][] outputs, int outputCount,
                int offset, int byteCount)
        {
            byte[][] table = Galois.MULTIPLICATION_TABLE;

            for (int iByte = offset; iByte < offset + byteCount; iByte++)
            {
                {
                    int iInput = 0;
                    byte[] inputShard = inputs[iInput];
                    byte inputByte = inputShard[iByte];
                    for (int iOutput = 0; iOutput < outputCount; iOutput++)
                    {
                        byte[] outputShard = outputs[iOutput];
                        byte[] matrixRow = matrixRows[iOutput];
                        byte[] multTableRow = table[matrixRow[iInput]];
                        outputShard[iByte] = multTableRow[inputByte];
                    }
                }
                for (int iInput = 1; iInput < inputCount; iInput++)
                {
                    byte[] inputShard = inputs[iInput];
                    byte inputByte = inputShard[iByte];
                    for (int iOutput = 0; iOutput < outputCount; iOutput++)
                    {
                        byte[] outputShard = outputs[iOutput];
                        byte[] matrixRow = matrixRows[iOutput];
                        byte[] multTableRow = table[matrixRow[iInput]];
                        outputShard[iByte] ^= multTableRow[inputByte];
                    }
                }
            }
        }
    }
}
