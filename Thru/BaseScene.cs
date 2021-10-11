using Nez;
using Nez.UI;

namespace Thru
{
    abstract class BaseScene : Scene
    {
        abstract public Table Table { get; set; }

        public BaseScene() { }

        public void SetupScene()
        {
            this.AddRenderer(new DefaultRenderer());

            var UICanvas = createEntity("ui-canvas").addComponent(new UICanvas());

            Table = UICanvas.stage.addElement(new Table());

            Table.setFillParent(true).top().padLeft(10).padTop(50);
        }
    }
}