﻿namespace Backblaze.Erasure
{
    public class ByteOutputInputExpCodingLoop : CodingLoopBase
    {
        public override void CodeSomeShards(
                byte[][] matrixRows,
                byte[][] inputs, int inputCount,
                byte[][] outputs, int outputCount,
                int offset, int byteCount)
        {
            for (int iByte = offset; iByte < offset + byteCount; iByte++)
            {
                for (int iOutput = 0; iOutput < outputCount; iOutput++)
                {
                    byte[] matrixRow = matrixRows[iOutput];
                    int value = 0;
                    for (int iInput = 0; iInput < inputCount; iInput++)
                    {
                        value ^= Galois.Multiply(matrixRow[iInput], inputs[iInput][iByte]);
                    }
                    outputs[iOutput][iByte] = (byte)value;
                }
            }
        }

    }
}
