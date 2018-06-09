open System
open System.Drawing
open System.Drawing.Imaging

let convertImageTo3D (sourceImage:string) (destinationImage:string) = 
    let bitmap = Bitmap.FromFile(sourceImage) :?> Bitmap   
    let w,h = bitmap.Width, bitmap.Height
    for x in 20 .. (w-1) do
        for y in 0 .. (h-1) do
            let c1 = bitmap.GetPixel(x,y)
            let c2 = bitmap.GetPixel(x - 20,y)
            let color3D = Color.FromArgb(int c1.R, int c2.G, int c2.B)
            bitmap.SetPixel(x - 20 ,y,color3D)
    bitmap.Save(destinationImage, ImageFormat.Jpeg)

let setGrayscale (sourceImage:string) (destinationImage:string) =   
    let bitmap = Bitmap.FromFile(sourceImage) :?> Bitmap    
    let w,h = bitmap.Width, bitmap.Height
    for x = 0 to (w-1) do
        for y = 0 to (h-1) do  
            let c = bitmap.GetPixel(x,y)
            let gray = int(0.299 * float c.R + 0.587 * float c.G + 0.114 * float c.B)
            bitmap.SetPixel(x,y, Color.FromArgb(gray, gray, gray))
    bitmap.Save(destinationImage, ImageFormat.Jpeg) 

let setRedscale (sourceImage:string) (destinationImage:string) =  
    let bitmap = Bitmap.FromFile(sourceImage) :?> Bitmap    
    let w,h = bitmap.Width, bitmap.Height
    for x = 0 to (w-1) do
        for y = 0 to (h-1) do   
            let c = bitmap.GetPixel(x,y)
            bitmap.SetPixel(x,y, Color.FromArgb(int c.R, 0, 0))
    bitmap.Save(destinationImage, ImageFormat.Jpeg) 


[<EntryPoint>]
let main argv =
    System.Threading.Tasks.Parallel.Invoke(
        Action(fun () -> convertImageTo3D "MonaLisa.jpg" "MonaLisa3D.jpg"),
        Action(fun () -> setGrayscale "LadyErmine.jpg" "LadyErmineRed.jpg"),
        Action(fun () -> setRedscale "GinevraBenci.jpg" "GinevraBenciGray.jpg"))
    0
