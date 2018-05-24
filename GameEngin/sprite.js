function Sprite(){
    this.x = 0;
    this.y = 0;
    this.width = 100;
    this.height = 100;
    this.alpha = 1;
    this.rotation = 0;
    this.anchorX = this.width/2;
    this.anchorY = this.height/2;
    this.og_width = this.width;
    this.og_height = this.height;
    this.image = null;
}

Sprite.prototype = {
    setImage: function(image){
        this.width = image.width;
        this.height = image.height;
        this.anchorX = this.width/2;
        this.anchorY = this.height/2;
        this.og_width = this.width;
        this.og_height = this.height;
        this.image = image;
    },
    clearImage: function(){
        this.image = null;
    },
    draw: function(context){
        if (this.width != this.og_width) {
            this.anchorX = this.width/2;
            this.og_width = this.width;
        }
        if (this.height != this.og_height) {
            this.anchorY = this.height/2;
            this.og_height = this.height;
        }
        context.save();
        context.translate(this.x + this.anchorX, this.y + this.anchorY);
        context.rotate(Math.PI * this.rotation/180);
        context.globalAlpha = this.alpha;
        context.fillStyle = "#CCCCCC";
        this.image?
        context.drawImage(this.image, -this.anchorX, -this.anchorY, this.width, this.height):
        context.fillRect(-this.anchorX, -this.anchorY, this.width, this.height);
        context.restore();
    }
};