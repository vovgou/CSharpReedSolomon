using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backblaze.Erasure
{
    public class InputByteOutputTableCodingLoop : CodingLoopBase
    {
        public override void CodeSomeShards(
           byte[][] matrixRows,
           byte[][] inputs, int inputCount,
           byte[][] outputs, int outputCount,
           int offset, int byteCount)
        {
            byte[][] table = Galois.MULTIPLICATION_TABLE;

            {
                int iInput = 0;
                byte[] inputShard = inputs[iInput];
                for (int iByte = offset; iByte < offset + byteCount; iByte++)
                {
                    byte inputByte = inputShard[iByte];
                    byte[] multTableRow = table[inputByte];
                    for (int iOutput = 0; iOutput < outputCount; iOutput++)
                    {
                        byte[] outputShard = outputs[iOutput];
                        byte[] matrixRow = matrixRows[iOutput];
                        outputShard[iByte] = multTableRow[matrixRow[iInput]];
                    }
                }
            }

            for (int iInput = 1; iInput < inputCount; iInput++)
            {
                byte[] inputShard = inputs[iInput];
                for (int iByte = offset; iByte < offset + byteCount; iByte++)
                {
                    byte inputByte = inputShard[iByte];
                    byte[] multTableRow = table[inputByte];
                    for (int iOutput = 0; iOutput < outputCount; iOutput++)
                    {
                        byte[] outputShard = outputs[iOutput];
                        byte[] matrixRow = matrixRows[iOutput];
                        outputShard[iByte] ^= multTableRow[matrixRow[iInput]];
                    }
                }
            }
        }
    }
}
