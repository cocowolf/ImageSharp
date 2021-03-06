﻿// <copyright file="GrayscaleTest.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Tests
{
    using System.IO;

    using Xunit;
    using ImageSharp.Processing;
    using ImageSharp.Tests;
    using System.Numerics;

    public class GrayscaleTest : FileTestBase
    {
        /// <summary>
        /// Use test patterns over loaded images to save decode time.
        /// </summary>
        [Theory]
        [WithTestPatternImages(50, 50, PixelTypes.StandardImageClass, GrayscaleMode.Bt709)] 
        [WithTestPatternImages(50, 50, PixelTypes.StandardImageClass, GrayscaleMode.Bt601)]
        public void ImageShouldApplyGrayscaleFilterAll<TColor>(TestImageProvider<TColor> provider, GrayscaleMode value)
            where TColor : struct, IPixel<TColor>
        {
            using (Image<TColor> image = provider.GetImage())
            {
                image.Grayscale(value);
                byte[] data = new byte[3];
                foreach (TColor p in image.Pixels)
                {
                    p.ToXyzBytes(data, 0);
                    Assert.Equal(data[0], data[1]);
                    Assert.Equal(data[1], data[2]);
                }

                image.DebugSave(provider);
            }
        }
    }
}