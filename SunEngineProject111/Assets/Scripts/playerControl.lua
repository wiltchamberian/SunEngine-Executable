x_speed = 5.0

state = 0       -- 0: 在地面, 1: 在空中
g = 20
y_speed = 12.0
y_initialSpeed =12.0

y_pos = 0.0

function Attach(obj)
end

function Update(obj, dt)
    -- play audio
    if Input.GetKeyDown(KeyCode.J) then
        AudioPlayer.Play2D("res://Musics/solid.wav", 1.0, false)
    end

    -- 1. 输入检测：跳跃
    if (Input.GetKeyDown(KeyCode.Space) or Input.GetKeyDown(KeyCode.K)) and state == 0 then
        state = 1;
        y_speed = y_initialSpeed -- 确保每次起跳时速度正确
    end

    -- 记录上一帧的位置，用来计算 delta_y
    local tmp_y = y_pos
    local dx = 0
    local dy = 0

    -- 2. 计算空中物理逻辑
    if state == 1 then
        y_speed = y_speed - g * dt
        y_pos = y_pos + y_speed * dt
        
        -- 落地判定：只有当速度向下（y_speed < 0）且坐标归零时才算落地
        if y_pos <= 0 and y_speed <= 0 then
            y_pos = 0
            y_speed = y_initialSpeed
            state = 0
        end
    end
    
    -- 计算这一帧真正需要的 Y 轴位移
    dy = y_pos - tmp_y

    -- 3. 输入检测：左右移动 (只计算 dx)
    if Input.GetKey(KeyCode.A) then
        dx = dx - x_speed * dt
    end

    if Input.GetKey(KeyCode.D) then
        dx = dx + x_speed * dt
    end
    
    -- 4. 统一应用位移 (无论是否按移动键，物理位移 dy 都会生效)
    obj:move(dx, dy, 0.0)
end

function Destroy()
end