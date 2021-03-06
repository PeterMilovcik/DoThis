﻿using Beeffective.Data;
using Beeffective.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Beeffective.Tests.Models.HoneycombModelTests
{
    class AddCell : TestFixture
    {
        public override void SetUp()
        {
            base.SetUp();
            CellModel1 = Sut.AddCell(CellModel1);
        }

        [Test]
        public void CellModel_Is_Added_To_Cells() => 
            Sut.Cells.Should().Contain(CellModel1);

        [Test]
        public void AddedCell_IsPersistent()
        {
            Repository = new CellRepository();
            Sut = new HoneycombModel(Repository);
            Sut.Load();
            Sut.Cells.Should().Contain(CellModel1);
        }
    }
}
