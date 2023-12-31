﻿namespace Backblaze.Erasure
{
    public abstract class CodingLoopBase : CodingLoop
    {
        public virtual bool CheckSomeShards(byte[][] matrixRows, byte[][] inputs, int inputCount, byte[][] toCheck, int checkCount, int offset, int byteCount, byte[] tempBuffer)
        {
            // This is the loop structure for ByteOutputInput, which does not
            // require temporary buffers for checking.
            byte[][] table = Galois.MULTIPLICATION_TABLE;
            for (int iByte = offset; iByte < offset + byteCount; iByte++)
            {
                for (int iOutput = 0; iOutput < checkCount; iOutput++)
                {
                    byte[] matrixRow = matrixRows[iOutput];
                    int value = 0;
                    for (int iInput = 0; iInput < inputCount; iInput++)
                    {
                        value ^= table[matrixRow[iInput] & 0xFF][inputs[iInput][iByte] & 0xFF];
                    }
                    if (toCheck[iOutput][iByte] != (byte)value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public abstract void CodeSomeShards(byte[][] matrixRows, byte[][] inputs, int inputCount, byte[][] outputs, int outputCount, int offset, int byteCount);
    }
}
