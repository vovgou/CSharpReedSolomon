namespace Backblaze.Erasure
{
    public class InputByteOutputExpCodingLoop : CodingLoopBase
    {
        public override void CodeSomeShards(
            byte[][] matrixRows,
            byte[][] inputs, int inputCount,
            byte[][] outputs, int outputCount,
            int offset, int byteCount)
        {
            {
                int iInput = 0;
                byte[] inputShard = inputs[iInput];
                for (int iByte = offset; iByte < offset + byteCount; iByte++)
                {
                    byte inputByte = inputShard[iByte];
                    for (int iOutput = 0; iOutput < outputCount; iOutput++)
                    {
                        byte[] outputShard = outputs[iOutput];
                        byte[] matrixRow = matrixRows[iOutput];
                        outputShard[iByte] = Galois.Multiply(matrixRow[iInput], inputByte);
                    }
                }
            }

            for (int iInput = 1; iInput < inputCount; iInput++)
            {
                byte[] inputShard = inputs[iInput];
                for (int iByte = offset; iByte < offset + byteCount; iByte++)
                {
                    byte inputByte = inputShard[iByte];
                    for (int iOutput = 0; iOutput < outputCount; iOutput++)
                    {
                        byte[] outputShard = outputs[iOutput];
                        byte[] matrixRow = matrixRows[iOutput];
                        outputShard[iByte] ^= Galois.Multiply(matrixRow[iInput], inputByte);
                    }
                }
            }
        }
    }
}
