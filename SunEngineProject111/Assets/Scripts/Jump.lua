height = 0.0
y = 1.0

function Attach(obj)

end

function Update(obj, dt)
    local speed = 20.0

    height = height + y * speed * dt

    if height > 20.0 then
        y = -1.0
    elseif height < 0.0 then
        y = 1.0
    end

    obj:move(0.0, y * dt * speed, 0.0)

    
end

function Destroy()

end

